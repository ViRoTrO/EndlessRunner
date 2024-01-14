using System.Collections;
using UnityEngine;


public class Player : BaseView
{
    [SerializeField] private string runState = "Running";
    [SerializeField] private string JumpState = "Jump";
    [SerializeField] private string moveRightState = "Right";
    [SerializeField] private string moveLeftState = "Left";
    [SerializeField] private string slideState = "Slide";
    [SerializeField] private string fallState = "Fall";
    [SerializeField] private Transform leftPosition;
    [SerializeField] private Transform centerPosition;
    [SerializeField] private Transform rightPosition;

    enum PlayerPositionEnum
    {
        Left,
        Center,
        Right,
    }

    private Animator _animator;
    private CharacterController _characterController;
    private Vector3 _targetPosition;
    private PlayerPositionEnum playerPosition;

    private float _groundPositionY;
    private float _upForce;
    private bool _isOnGround;
    private bool _isJump;
    private bool _isSliding;

    private void Start()
    {
        SignalService.Subscribe<SwipeDetectionSignal>(SwipeDirection);
        SignalService.Subscribe<GameStateChanged>(OnGameStateChange);

        _animator = GetComponent<Animator>();
        _characterController = GetComponent<CharacterController>();
        _groundPositionY = gameObject.transform.position.y;
        _targetPosition = transform.position;
        playerPosition = PlayerPositionEnum.Center;
        _animator.Play(runState);
    }



    private void SwipeDirection(SwipeDetectionSignal val)
    {
        switch (val.Direction)
        {
            case SwipeDirectionEnums.Up:
                Jump();
                break;
            case SwipeDirectionEnums.Down:
                Slide();
                break;
            case SwipeDirectionEnums.Right:
                MoveRight();
                break;
            case SwipeDirectionEnums.Left:
                MoveLeft();
                break;

        }
    }

    private void OnGameStateChange(GameStateChanged gameState)
    {
        if(gameState.GameState == GameStateEnum.Playing)
            SignalService.Fire(new PlayAudioInLoop(){audioClip = SoundNamesEnums.Run});
        else if(gameState.GameState == GameStateEnum.GamePause || gameState.GameState == GameStateEnum.GameOver)
            SignalService.Fire(new StopAudio());

    }

    private void Update()
    {
        if (gameObject.transform.position.y <= _groundPositionY)
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, _groundPositionY, gameObject.transform.position.z);

        if (_isJump)
        {
            GetComponent<Rigidbody>().velocity = new Vector3(0, GameInfoSO.JumpPower, 0);
            _isJump = false;
        }

        transform.position = Vector3.Lerp(transform.position, _targetPosition, GameInfoSO.CharacterSpeed * Time.deltaTime);

    }

    private void MoveRight()
    {
        _animator.Play(moveRightState);

        if (playerPosition == PlayerPositionEnum.Center)
        {
            _targetPosition = rightPosition.position;
            playerPosition = PlayerPositionEnum.Right;
        }
        else if (playerPosition == PlayerPositionEnum.Left)
        {
            _targetPosition = centerPosition.position;
            playerPosition = PlayerPositionEnum.Center;
        }
    }

    private void MoveLeft()
    {
        _animator.Play(moveLeftState);

        if (playerPosition == PlayerPositionEnum.Center)
        {
            _targetPosition = leftPosition.position;
            playerPosition = PlayerPositionEnum.Left;
        }
        else if (playerPosition == PlayerPositionEnum.Right)
        {
            _targetPosition = centerPosition.position;
            playerPosition = PlayerPositionEnum.Center;
        }
    }

    private void Slide()
    {
        _animator.Play(slideState);
        _isSliding = true;
        StartCoroutine(WaitForSLideTOEnd());
        SignalService.Fire(new PlayAudio(){audioClip = SoundNamesEnums.Slide});
    }

    private IEnumerator WaitForSLideTOEnd()
    {
        yield return new WaitForSeconds(0.9f);
        _isSliding = false;
    }

    private void Jump()
    {
        _animator.Play(JumpState);
        _isJump = true;
        StartCoroutine(WaitForJumpToEnd());
        SignalService.Fire(new StopAudio());
    }

    private IEnumerator WaitForJumpToEnd()
    {
        yield return new WaitForSeconds(1f);
        SignalService.Fire(new PlayAudioInLoop(){audioClip = SoundNamesEnums.Run});
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (Model.CurrentGameState != GameStateEnum.Playing)
            return;

        var tag = collision.gameObject.tag;
        if (tag == "Coin")
        {
            SignalService.Fire(new CoinsCollected());
            SignalService.Fire(new PlayAudio(){audioClip = SoundNamesEnums.CoinCollect});
            return;
        }

        if (tag == "Obstacle" || (tag == "ObstacleSlide" && !_isSliding))
        {
            SignalService.Fire(new PlayerHitObstacle());
            _animator.Play(fallState);
            SignalService.Fire(new PlayAudio(){audioClip = SoundNamesEnums.HitObstacle});
        }
    }
}


