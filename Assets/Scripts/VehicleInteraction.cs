using UnityEngine;

/// <summary>
/// مدیریت ورود و خروج کاراکتر به خودرو و جابجایی کنترل‌ها و دوربین.
/// </summary>
public class VehicleInteraction : MonoBehaviour
{
    [Header("Dependencies")]
    [Tooltip("اسکریپت کنترل فیزیک بت‌موبیل")]
    public BatmobileController carController;

    [Tooltip("دوربین خودرو")]
    public Camera carCamera;

    [Tooltip("محل قرارگیری کاراکتر هنگام رانندگی")]
    public Transform driverSeat;

    [Header("Interaction Settings")]
    public KeyCode interactKey = KeyCode.E;
    public KeyCode exitKey = KeyCode.G;
    
    // وضعیت فعلی
    private bool isPlayerNearby = false;
    private bool isPlayerDriving = false;
    private GameObject player;
    private BatmanController playerController;
    private Collider playerCollider;

    void Start()
    {
        // در شروع، کنترل خودرو باید غیرفعال باشد
        carController.enabled = false;
        carCamera.enabled = false;
    }

    void Update()
    {
        if (isPlayerNearby && Input.GetKeyDown(interactKey) && !isPlayerDriving)
        {
            EnterVehicle(player);
        }
        else if (isPlayerDriving && Input.GetKeyDown(exitKey))
        {
            ExitVehicle();
        }
    }

    /// <summary>
    /// وقتی کاراکتر وارد تریگر می‌شود
    /// </summary>
    private void OnTriggerEnter(Collider other)
    {

            Debug.Log($"Press {interactKey} to enter the Batmobile.");
        if (other.CompareTag("Player")) // مطمئن شوید آبجکت Batman تگ "Player" دارد
        {
            isPlayerNearby = true;
            player = other.gameObject;
            playerController = player.GetComponent<BatmanController>();
            Debug.Log($"Press {interactKey} to enter the Batmobile.");
        }
    }

    /// <summary>
    /// وقتی کاراکتر از تریگر خارج می‌شود
    /// </summary>
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
            player = null;
            playerController = null;
        }
    }

    /// <summary>
    /// منطق سوار شدن به ماشین
    /// </summary>
    private void EnterVehicle(GameObject character)
    {
        if (character == null || playerController == null) return;
        playerCollider = character.GetComponent<Collider>();
        if (playerCollider != null)
        {
            playerCollider.enabled = false; // <<< نکته کلیدی
        }

        isPlayerDriving = true;

        // ۱. غیرفعال کردن کنترل و فیزیک کاراکتر
        playerController.SetControlActive(false); // تابعی که باید در PatrolController بسازیم
        character.GetComponent<Rigidbody>().isKinematic = true; // فیزیک کاراکتر را غیرفعال کن

        // ۲. قرار دادن کاراکتر در صندلی
        character.transform.SetParent(driverSeat);
        character.transform.localPosition = Vector3.zero;
        character.transform.localRotation = Quaternion.identity;

        // ۳. فعال کردن کنترل ماشین
        carController.enabled = true;

        // ۴. سوئیچ دوربین (اگر از CameraSwitcher استفاده می‌کنید، آن را اینجا صدا بزنید)
        carCamera.enabled = true;
        // فرض کنید دوربین‌های کاراکتر را غیرفعال می‌کنید
        // Camera.main.enabled = false; 

        // ماوس را قفل کن (برای BatmobileController)
        Cursor.lockState = CursorLockMode.Locked;
    }

    /// <summary>
    /// منطق پیاده شدن از ماشین
    /// </summary>
    private void ExitVehicle()
    {
        if (player == null || playerController == null) return;

        isPlayerDriving = false;

        // ۱. غیرفعال کردن کنترل ماشین
        carController.enabled = false;
        if (playerCollider != null)
        {
            playerCollider.enabled = true; // <<< فعال کردن مجدد
        }

        // ۲. خارج کردن کاراکتر از صندلی
        player.transform.SetParent(null); // از Parent خارج کن
        player.transform.position = transform.position + transform.right * 2f; // کمی دورتر پیاده کن
        player.transform.position = Vector3.up * 1.3f; // کمی بالاتر برای جلوگیری از گیر کردن در زمین
        player.transform.rotation = transform.rotation;
        
        // ۳. فعال کردن کنترل و فیزیک کاراکتر
        playerController.SetControlActive(true);
        player.GetComponent<Rigidbody>().isKinematic = false; // فیزیک کاراکتر را فعال کن

        // ۴. سوئیچ دوربین
        carCamera.enabled = false;
        // Camera.main.enabled = true; 
        
        // ماوس را آزاد کن (اگر نیاز است)
        Cursor.lockState = CursorLockMode.None;
    }
}