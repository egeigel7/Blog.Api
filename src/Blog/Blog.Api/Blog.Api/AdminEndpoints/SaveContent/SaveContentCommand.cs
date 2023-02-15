using Blog.Api.Domain;

namespace Blog.Api.AdminEndpoints.SaveContent
{
    public class SaveContentCommand: ICommand
    {
        public SaveContentRequest Request { get; set; }
        public SaveContentCommand(SaveContentRequest request)
        {
            Request= request;
        }
    }
}
