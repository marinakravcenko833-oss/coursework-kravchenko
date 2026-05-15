using Coursework.BLL;
using Coursework.DAL;
using Coursework.UI;

namespace Coursework;

class Program
{
    static void Main(string[] args)
    {
        var repository = new FileTransactionRepository();
        var service = new FinanceService(repository);
        var menu = new ConsoleMenu(service);

        menu.Run();
    }
}