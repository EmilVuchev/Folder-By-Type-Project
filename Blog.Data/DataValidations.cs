namespace Blog.Data
{
    public class DataValidations
    {
        public class Article 
        {
            public const int MaxTitleLength = 50;

            public const int MinTitleLength = 10;

            public const int MaxDescriptionLength = 250;

            public const int MinDescriptionLength = 50;
        }
    }
}
