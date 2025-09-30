using UnityEngine;

public class DestructableObject : Entity, IInteractable
{
    public string ShowMessage;
    public void Interact(GameObject observer)
    {
        print(observer.name + "A interactuado conmigo");
        Destroy(gameObject);
    }
}