using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    public Transform Target;

    public void Restart()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Game");
    }

    private void Update()
    {
        Vector3 transformPosition = transform.position;
        transformPosition.x = Target.position.x;
        transformPosition.z = Target.position.z;
        transform.position = transformPosition;
    }
}
