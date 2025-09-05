namespace ApplicationCommon
{
    public class CommonMessages
    {
        public enum Mess
        {
            NotFound = 1,
            DuplicateEntry = 2,
            InvalidOperation = 3,
            Unauthorized = 4,
            Forbidden = 5,
            BadRequest = 6,
            InternalServerError = 7,
            ServiceUnavailable = 8,
            UpdateFailed = 9,
            CreateFailed = 10,
            UpdateSuceeded = 11,
            CreateSucceeded = 12,
            DeleteFailed = 13,
            RetrievalFailed=14
        }

        public string GetFriendlyMessage(Mess exceptionType)
        {
            string Message = "";
            switch (exceptionType)
            {
                case (Mess.NotFound):
                    Message = "Not Found";
                    break;

                case (Mess.DuplicateEntry):
                    Message = "Duplicate Entry";
                    break;
                case (Mess.UpdateFailed):
                    Message = "Error occurred while updating.";
                    break;
                case (Mess.CreateFailed):
                    Message = "Error occurred while creating new.";
                    break;
                case (Mess.DeleteFailed):
                    Message = "Error occurred while deleting.";
                    break;
                case (Mess.CreateSucceeded):
                    Message = "Created successfully.";
                    break;
                case (Mess.UpdateSuceeded):
                    Message = "Updated successfully.";
                    break;
                case (Mess.RetrievalFailed):
                    Message = "Failed to retrieve the records.";
                    break;
                default:
                    Message = "Bad Request";
                    break;

            }
            return Message;
        }

        public string GetFriendlyMessage(string exceptionType)
        {
            string Message = "";
            switch (exceptionType)
            {
                case ("1"):
                    Message = "Not Found";
                    break;

                case ("2"):
                    Message = "Duplicate Entry";
                    break;
                default:
                    Message = "Bad Request";
                    break;

            }
            return Message;
        }
    }
}
