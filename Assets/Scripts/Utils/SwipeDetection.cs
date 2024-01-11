using UnityEngine;

public class SwipeDetection : BaseView
{
    private Vector2 fingerDownPosition;
    private Vector2 fingerUpPosition;

    public float minSwipeDistance = 20f;

    private void Update()
    {
        if (Input.touchSupported)
        {
            TouchDetect();
            return;
        }

        MouseDetect();
    }

    private void TouchDetect()
    {

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            // Record the starting position of the swipe
            fingerDownPosition = Input.GetTouch(0).position;
        }


        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {

            fingerUpPosition = Input.GetTouch(0).position;
            Vector2 swipeDirection = fingerUpPosition - fingerDownPosition;

            if (swipeDirection.magnitude >= minSwipeDistance)
            {

                swipeDirection.Normalize();

                if (Mathf.Abs(swipeDirection.y) > Mathf.Abs(swipeDirection.x))
                {
                    if (swipeDirection.y > 0)
                    {
                        SignalService.Fire(new SwipeDetectionSignal()
                        {
                            Direction = SwipeDirectionEnums.Up
                        });
                    }
                    else if (swipeDirection.y < 0)
                    {
                        SignalService.Fire(new SwipeDetectionSignal()
                        {
                            Direction = SwipeDirectionEnums.Down
                        });
                    }
                }
                else
                {
                    if (swipeDirection.x > 0)
                    {
                        SignalService.Fire(new SwipeDetectionSignal()
                        {
                            Direction = SwipeDirectionEnums.Right
                        });
                    }
                    else if (swipeDirection.x < 0)
                    {
                        SignalService.Fire(new SwipeDetectionSignal()
                        {
                            Direction = SwipeDirectionEnums.Left
                        });
                    }
                }
            }
        }
    }

    private void MouseDetect()
    {

        if (Input.GetMouseButtonDown(0))
        {
            fingerDownPosition = Input.touchCount > 0 ? Input.GetTouch(0).position : (Vector2)Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(0))
        {

            fingerUpPosition = (Vector2)Input.mousePosition;

            Vector2 swipeDirection = fingerUpPosition - fingerDownPosition;

            if (swipeDirection.magnitude >= minSwipeDistance)
            {

                swipeDirection.Normalize();

                if (Mathf.Abs(swipeDirection.y) > Mathf.Abs(swipeDirection.x))
                {
                    if (swipeDirection.y > 0)
                    {
                        SignalService.Fire(new SwipeDetectionSignal()
                        {
                            Direction = SwipeDirectionEnums.Up
                        });
                    }
                    else if (swipeDirection.y < 0)
                    {
                        SignalService.Fire(new SwipeDetectionSignal()
                        {
                            Direction = SwipeDirectionEnums.Down
                        });
                    }
                }
                else
                {
                    if (swipeDirection.x > 0)
                    {
                        SignalService.Fire(new SwipeDetectionSignal()
                        {
                            Direction = SwipeDirectionEnums.Right
                        });
                    }
                    else if (swipeDirection.x < 0)
                    {
                        SignalService.Fire(new SwipeDetectionSignal()
                        {
                            Direction = SwipeDirectionEnums.Left
                        });
                    }
                }
            }
        }
    }
}
