using Demo.Abstractions.Domain.Entities;
using Demo.Abstractions.Presentation.ViewModels;
using GalaSoft.MvvmLight;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAValidationResult = System.ComponentModel.DataAnnotations.ValidationResult;

namespace Demo.Presentation.ViewModels
{
    public abstract class EntityViewModel<T> : ViewModelBase, 
        INotifyDataErrorInfo, IEntityViewModel<T> 
        where T : IEntity
    {
        private readonly Dictionary<string, ValidationResults> _errors = new Dictionary<string, ValidationResults>();
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        public virtual int Id { get; set; }


        public bool HasErrors => false;

        public EntityViewModel()    
        {
            ErrorsChanged += ErrorsChanged;
        }

        public IEnumerable GetErrors(string propertyName)
        {
            if (propertyName == null || !_errors.ContainsKey(propertyName)) return null;

            return _errors[propertyName].Aggregate(new List<DAValidationResult>(), (l, vr) =>
            {
                var validationResult = new DAValidationResult(vr.Message);
                l.Add(validationResult);

                return l;
            });
        }
        protected virtual void OnErrorsChanged(object sender, DataErrorsChangedEventArgs e)
        {
        }

        public abstract void CopyTo(T entity);

    }
}
