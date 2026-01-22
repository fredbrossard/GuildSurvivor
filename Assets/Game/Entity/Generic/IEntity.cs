namespace Game.Entity.Generic
{
    public interface IEntity<T> where T : IEntityModel
    {
        public bool IsAlive { get; set; }
        public T Model { get; }

        public void Initialize();
        public void Enable();
        public void Disable();
    }
}