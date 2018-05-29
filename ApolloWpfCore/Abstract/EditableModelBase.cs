using ApolloWpfCore.Extensions;
using GalaSoft.MvvmLight.Command;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace ApolloWpfCore.Abstract
{
    /// <summary>
    /// Classe abstraite englobante d'observation d'un objet business, basé sur MVVM light
    /// avec une gestion du changement
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class EditableModelBase<T> : ModelBase<T>, IEditableObject where T : class
    {
        protected EditableModelBase(T obj) : base(obj)
        {
            BeginEditCommand = new RelayCommand(BeginEdit);
            CancelEditCommand = new RelayCommand(CancelEdit);
            EndEditCommand = new RelayCommand(EndEdit);
        }

        private T _backup;
        private bool _isModified;

        public bool IsModified
        {
            get => _isModified;
            set => Set(ref _isModified, value);
        }

        public ICommand BeginEditCommand { get; }
        public ICommand CancelEditCommand { get; }
        public ICommand EndEditCommand { get; }


        /// <summary>
        /// Notifie le changement de propriété ainsi que l'état de modification. exemples d'utilisation
        /// set => Edit(() => Instance.Username = value);
        /// 
        /// </summary>
        public void Edit(Action PropertyAssignment, [CallerMemberName] string propertyName = null)
        {
            BeginEdit();
            PropertyAssignment.Invoke();
            RaisePropertyChanged(propertyName);
        }

        public void BeginEdit()
        {
            if (_backup == null)
            {
                _backup = Instance.Copy();
                IsModified = true;
            }
        }

        public void CancelEdit()
        {
            if (_backup != null)
            {
                Instance = _backup;
                RaisePropertyChanged(null);
                _backup = null;
                IsModified = false;
                OnCancelEdit?.Invoke(Instance);
            }
        }

        public void EndEdit()
        {
            if (_backup != null)
            {
                _backup = null;
                IsModified = false;
                OnEndEdit?.Invoke(Instance);
            }
        }

        /// <summary>
        /// Evenement associé a l'action d'annulation du changement
        /// </summary>
        public event Action<T> OnCancelEdit;

        /// <summary>
        /// Evenuement associé a l'action de sauvegarde (fin d'édition)
        /// </summary>
        public event Action<T> OnEndEdit;
    }
}
