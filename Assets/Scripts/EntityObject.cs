using UnityEngine;

public class EntityObject : Entity, IInteractable
{
    public string ShowMessage;
    public void Interact(GameObject observer)
    {
        print(observer.name + "A interactuado conmigo");
        print(ShowMessage);
        
    }
}
