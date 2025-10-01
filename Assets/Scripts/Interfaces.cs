using UnityEngine;

public interface IDamageable
{
    public void TakeDamage(int damage , Vector3 origin);
}
public interface IInteractable
{
    //-> Observer es la persona que esta interactuando conmigo!!!
    public void Interact(GameObject observer);
}
