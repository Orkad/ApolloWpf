using System.Collections.ObjectModel;
using System.Windows.Input;

namespace ApolloWpfCore.Abstract
{
    public class EditableCollectionBase<T> : ObservableCollection<EditableModelBase<T>> where T : class
    {
        public ICommand AddCommand { get; }
        public ICommand RemoveCommand { get; }

        public EditableCollectionBase()
        {

        }
    }
}
