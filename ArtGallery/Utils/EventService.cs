namespace ArtGallery.Utils
{
    public class EventService
    {
        public event Func<Task> OnPeriodDeleted;
        public event Func<Task> OnPeriodUpdate;

        public async Task NotifyPeriodUpdate(int id)
        {
            Console.WriteLine("Notify Update");
            if (OnPeriodUpdate != null)
            {
                foreach (var handler in OnPeriodUpdate.GetInvocationList())
                {
                    await ((Func<Task>)handler).Invoke();
                }
            }
        }


        public async Task NotifyPeriodDeleted()
        {
            Console.WriteLine("Notify Deleted");
            if (OnPeriodDeleted != null)
            {
                foreach (var handler in OnPeriodDeleted.GetInvocationList())
                {
                    await ((Func<Task>)handler).Invoke();
                }
            }
        }
    }
}
