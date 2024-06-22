namespace ArtGallery.Utils
{
    public class EventService
    {
        public event Func<Task> OnPeriodDeleted;

        public async Task NotifyPeriodDeleted()
        {
            Console.WriteLine("Notify Deleted");
            if (OnPeriodDeleted != null)
            {
                foreach (var handler in OnPeriodDeleted.GetInvocationList())
                {
                    await((Func<Task>)handler).Invoke();
                }
            }
        }
    }
}
