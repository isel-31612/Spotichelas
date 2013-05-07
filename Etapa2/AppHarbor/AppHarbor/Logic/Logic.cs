using DataAccess;

namespace BusinessRules
{
    public class Logic
    {
        private static Logic singleton;

        protected Logic()
        {
            DAL dal = DAL.Factory();
            Create = new CreateLogic(dal);
            Edit = new EditLogic(dal);
            Find = new FindLogic(dal);
            FindAll = new FindAllLogic(dal);
            Remove = new RemoveLogic(dal);
        }

        public static Logic Factory()
        {
            if (singleton == null)
                singleton = new Logic();//TODO: dependency injector
            return singleton;
        }

        public CreateLogic Create { get; set; }
        public EditLogic Edit { get; set; }
        public FindLogic Find { get; set; }
        public FindAllLogic FindAll { get; set; }
        public RemoveLogic Remove { get; set; }
    }
}
