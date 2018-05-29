using System.Collections.ObjectModel;

namespace ApolloWpfCore.Abstract
{
    public class EditableCollectionBase<T> : ObservableCollection<EditableModelBase<T>> where T : class
    {
        
    }
}
