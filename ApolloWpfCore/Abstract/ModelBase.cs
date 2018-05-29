using GalaSoft.MvvmLight;
using System;

namespace ApolloWpfCore.Abstract
{
    /// <inheritdoc />
    /// <summary>
    /// Classe abstraite englobante d'observation d'un objet business basé sur MVVM light<para />
    /// Les classes filles devront implémenter les propriétés dont le changement est a notifier<para />
    /// </summary>
    /// <typeparam name="T">type de l'objet business</typeparam>
    public abstract class ModelBase<T> : ObservableObject where T : class
    {
        private T _instance;

        /// <summary>
        /// Construit l'instance de la classe d'observation pour l'objet business passé en paramètre
        /// </summary>
        /// <param name="obj">instance de </param>
        protected ModelBase(T obj)
        {
            Instance = obj;
        }

        /// <summary>
        /// Instance de l'objet business observé (ne sera jamais null)
        /// </summary>
        protected T Instance
        {
            get => _instance;
            set
            {
                if (value == null)
                {
                    throw new ArgumentException("l'instance de l'objet business ne peux être null");
                }
                Set(ref _instance, value);
            }
        }
    }
}
