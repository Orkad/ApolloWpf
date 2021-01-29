using GalaSoft.MvvmLight;

namespace Apollo.MVVM
{
    public class ObservableNamedValue<TValue> : ObservableObject
    {
        private string name;
        private TValue value;

        /// <summary>
        /// Nom d'affichage de la donnée
        /// </summary>
        public string Name { get => name; set => Set(ref name, value); }

        /// <summary>
        /// Valeur de la donnée
        /// </summary>
        public TValue Value { get => value; set => Set(ref this.value, value); }
    }
}
