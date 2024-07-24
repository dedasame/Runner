
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Controller : MonoBehaviour
{
    public static Controller Instance;

    public float forwardSpeed = 10.0f;

    [SerializeField] public GameObject floor1;
    [SerializeField] public GameObject floor2;
    public bool floorNum=true; //false = 1 - true = 2
    public float tempPos;

    public float xPos;
    public float yPos;
    public bool jump;
    public float counter;

    public float coinNum;

    [SerializeField] public Text coinText;
    [SerializeField] public Text trapText;
    [SerializeField] public Button restartBut;
    [SerializeField] public Text roadText;
    [SerializeField] public Button backBut;
    [SerializeField] public Button homeBut;

    public Animator animator; //animasyondaki degiskenlere erismek icin

    public float acc=1;

    public bool isFailed;

    public float jumpCounter = 1;

    public float timeScale;

    public bool doubleJump = false;
    public float maxJumpHeigh = 1;
    public float doubleJumpSpeed = 1;

    public float loadCoinNum;     
    public float highestRoad;


    [SerializeField] public AudioSource backgroundSound;
    [SerializeField] public AudioSource coinSound;
    [SerializeField] public AudioSource deathSound;
    [SerializeField] public AudioSource jumpSound;
    [SerializeField] public AudioSource buttonSound;

    [SerializeField] public AudioSource goSound;


    public int soundActive = 1;
    public int musicActive = 1;

    Vector3 firstMousePos;
    Vector3 lastMousePos;
    Vector3 diffMosuePos;
    public bool swipeLeft, swipeRight, swipeUp;


    public ParticleSystem starRing;
    public ParticleSystem highScore;
    public Text highText;
    public bool firstTime;
    

    private void Awake()
    {
            Instance = this;
    }

    

    void Start() 
    {

        tempPos = transform.position.z; //baslangic pozisyonu   
        
        loadCoinNum  = PlayerPrefs.GetFloat("sumCoin");     
        highestRoad = PlayerPrefs.GetFloat("highestRoad");
       
        
        soundActive = PlayerPrefs.GetInt("soundActive");
        musicActive = PlayerPrefs.GetInt("musicActive");


        if (musicActive == 1)
        {
            backgroundSound.Play();
        }
        else
        {
            backgroundSound.Stop();

        }

        if(soundActive == 1)
        {
            goSound.Play();
        }
        else
        {
            goSound.Stop();
        }

        starRing.Stop();
        highScore.Stop();
        firstTime = true;

    }


    void Update()
    {

        //Time.timeScale = timeScale;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Break();
        }

        if (isFailed)
        {
            return;
        }

        //max skor < þu anki skor -> þu anki skoru max skor diye kayit et.
        if (highestRoad < transform.position.z)
        {
            PlayerPrefs.SetFloat("highestRoad", transform.position.z);

            if (firstTime){
                highScore.Play();
                highText.gameObject.SetActive(true);
                Invoke("StopHigh", 2f); //2 saniye
                firstTime = false;
            }
            
        }


        if (Input.GetMouseButtonDown(0))
        {
            firstMousePos = Input.mousePosition;
        }
        
        if(Input.GetMouseButtonUp(0))
        {
            lastMousePos= Input.mousePosition;
            diffMosuePos = lastMousePos - firstMousePos;
            if (diffMosuePos.sqrMagnitude > 1) //ufak bir elini bas cek yaptiginda hareket etmemesi icin
            {

                if (Mathf.Abs(diffMosuePos.x) > Mathf.Abs(diffMosuePos.y))
                {
                    if (diffMosuePos.x < 0) swipeLeft = true;
                    if (diffMosuePos.x > 0) swipeRight= true;
                }
                else
                {
                    if (diffMosuePos.y > 0) swipeUp = true;
                }
            }
        }


        acc = Mathf.Clamp(Time.deltaTime * .2f + acc, 0, 4);
        transform.position = new Vector3(Mathf.Lerp(transform.position.x, xPos, .3f), Mathf.Clamp(yPos * counter * jumpCounter, 0, maxJumpHeigh), transform.position.z + forwardSpeed * acc * Time.deltaTime);
  
            if (swipeRight)
            {
                swipeRight = false;
                if (xPos == 1) { }
                else xPos += 1;

                diffMosuePos = new Vector3(0, 0, 0);
            }


            if (swipeLeft)
            {
            swipeLeft = false;
                if (xPos == -1) { }
                else xPos -= 1;

                diffMosuePos = new Vector3(0, 0, 0);
            }

        
            if (swipeUp)
            {
            swipeUp = false;
             
               
                    jumpSound.Play();
                

                if (jump == false && transform.position.y == 0)
                {
                    jump = true;
                    yPos = 1;
                    counter = 0;
                    animator.SetTrigger("jump");
                }

                else if (jump == true && doubleJump == false) //double jump
                {
                    doubleJump = true;
                    //o andaki yükseklik + 1 daha ekleyerek ziplayacaðýz.
                    var currentHigh = transform.position.y;
                    maxJumpHeigh = 2; //ziplama yüksekliðini arttýr. 

                }

                if (jump || doubleJump) return;

                diffMosuePos = new Vector3(0, 0, 0);

            }

            if (diffMosuePos.y<0)
            {
                
                jumpCounter = 5;
                var currentCounter = counter / jumpCounter;
                jump = false;
                doubleJump = false;
                if (counter * jumpCounter > 1)
                {
                    counter = currentCounter;
                    //Debug.Log("current counter " + currentCounter);
                    //Debug.Break();
                }
                diffMosuePos = new Vector3(0, 0, 0);

            }

            if (jump || doubleJump)
            {
                if (doubleJump) doubleJumpSpeed = 5;
                counter += Time.deltaTime * acc * doubleJumpSpeed;

                if (counter > 1) jump = false;
                if (counter > 2)
                {
                    doubleJump = false;
                    doubleJumpSpeed = 1;
                }
            }

            if (counter > 0 && !jump && !doubleJump)
            {

                counter -= Time.deltaTime * acc;
                if (counter < 0)
                {
                    counter = 0;
                    jumpCounter = 1;


                }
            }



        if ( Math.Abs(tempPos - transform.position.z ) > 100)
        {
            //yolu uzatma
            if (floorNum)
            {
                floor2.transform.position = new Vector3(floor2.transform.position.x, floor2.transform.position.y, floor2.transform.position.z + 200);
                floorNum =false;
            }

            else if(!floorNum)
            {
                floor1.transform.position = new Vector3(floor1.transform.position.x, floor1.transform.position.y, floor1.transform.position.z + 200);
                floorNum = true;
            }    

            tempPos = transform.position.z; //temp yenile
        }

        roadText.text = transform.position.z.ToString("F2") + "m"; //kaç m gittiðini yaziyor.

    }


    //collect coin
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("coin"))
        {
            if (soundActive == 1)
            {
                coinSound.Play(); 
            }
           

            Animator coinAnimator = other.gameObject.GetComponentInChildren<Animator>(); 

            coinAnimator.SetTrigger("collected");

            Destroy(other.gameObject,2); 

            coinNum++;
            coinText.text = "COIN: " + coinNum;

            //Her coin toplandiginda savelenmis coin sayisina+1 ekleriz ve tekrar kayit ederiz.

            loadCoinNum = PlayerPrefs.GetFloat("sumCoin");
            loadCoinNum += 1;
            PlayerPrefs.SetFloat("sumCoin", loadCoinNum);
            
        }

        if (other.gameObject.CompareTag("trap")) 
        {
            if (soundActive == 1)
            {
                deathSound.Play();
            }

            ////YILDIZ DONDURME 
            starRing.Play(); //Olmadi!!!!
            animator.SetTrigger("death");
            forwardSpeed = 0;
            trapText.gameObject.SetActive(true);
            restartBut.gameObject.SetActive(true);
            //Time.timeScale = 0;
            isFailed = true;

            

        }

    }

    public void Restart()
    {
        if(soundActive == 1)
        {
            buttonSound.Play(); //ses kontrol
        }

        
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Home()
    {
        if (soundActive == 1)
        {
            buttonSound.Play(); //ses kontrol
        }
       
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        
    }


    public void Back()
    {
        if (soundActive == 1)
        {
            buttonSound.Play(); //ses kontrol
        }
        restartBut.gameObject.SetActive(false);
        backBut.gameObject.SetActive(false);
        homeBut.gameObject.SetActive(false);
        isFailed = false;
    }

    public void Setting()
    {
        if (soundActive == 1)
        {
            buttonSound.Play(); //ses kontrol
        }
        isFailed = true;
        //restart - back - home buttons
        restartBut.gameObject.SetActive(true);
        backBut.gameObject.SetActive(true);
        homeBut.gameObject.SetActive(true);
    }


    void StopHigh()
    {
        highScore.Stop();
        highText.gameObject.SetActive(false);
    }

}
