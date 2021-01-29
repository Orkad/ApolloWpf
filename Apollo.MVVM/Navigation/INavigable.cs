namespace Apollo.MVVM.Navigation
{
    /// <summary>
    /// Détermine (pour un ViewModel) qu'on peut y naviguer dessus
    /// Permet de faire passer des paramètres
    /// </summary>
    public interface INavigable
    {
        void OnNavigatedTo(object parameter = null);
    }
}
