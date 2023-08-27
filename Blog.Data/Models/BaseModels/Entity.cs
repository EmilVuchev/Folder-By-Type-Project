namespace Blog.Data.Models.BaseModels
{
    public abstract class Entity<T>
        where T : struct
    {
        public T Id { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
