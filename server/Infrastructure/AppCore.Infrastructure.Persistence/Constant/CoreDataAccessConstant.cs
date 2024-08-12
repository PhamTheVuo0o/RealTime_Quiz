namespace AppCore.Infrastructure.Persistence.Constant
{
    public static class CoreDataAccessConstant
    {
        public const string PROPERTY_ID = "Id";
        public const int DEFAULT_DELETE_BATCH_SIZE = 50;
        public const int DEFAULT_IN_QUERY_BATCH_SIZE = 200;

        public const int DEFAULT_SMALL_PROCESSING_BLOCK_SIZE = 500;
        public const int DEFAULT_NORMAL_PROCESSING_BLOCK_SIZE = 1000;
        public const int DEFAULT_BIG_PROCESSING_BLOCK_SIZE = 2000;

        public const int DEFAULT_COMMAND_TIMEOUT_IN_SECONDS = 300;
        public const int DEFAULT_LONG_COMMAND_TIMEOUT_IN_SECONDS = 3600;

        public const int DEFAULT_RETRY_ON_FAILURE = 5;
        public const int DEFAULT_RETRY_ON_FAILURE_DELAY_IN_SECONDS = 3;

        public const int DEFAULT_DB_RETRY_ON_FAILURE = 5;
        public const int DEFAULT_DB_RETRY_ON_FAILURE_DELAY_IN_SECONDS = 1000;
    }
}
