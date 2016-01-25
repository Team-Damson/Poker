namespace Poker.Interfaces
{
    using System.Windows.Forms;

    public interface IPot
    {
        TextBox TextBox { get; }

        int Amount { get; }

        void Set(int amount);

        void Add(int amount);

        void Clear();
    }
}
