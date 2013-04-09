using DataAccess;

namespace BusinessRules
{
    public class Logic
    {
        private static Logic singleton;

        private Logic()
        {
            DAL dal = DAL.Factory();//TODO: inject repository
            Create = new CreateLogic(dal);
            Edit = new EditLogic(dal);
            Find = new FindLogic(dal);
            FindAll = new FindAllLogic(dal);
            Remove = new RemoveLogic(dal);
        }

        public static Logic Factory()
        {
            if (singleton == null)
                singleton = new Logic();
            return singleton;
        }

        public CreateLogic Create { get; set; }
        public EditLogic Edit { get; set; }
        public FindLogic Find { get; set; }
        public FindAllLogic FindAll { get; set; }
        public RemoveLogic Remove { get; set; }
    }
}
