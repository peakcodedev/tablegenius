using System;
using System.Collections.Generic;

namespace TableGenius.Api.Presentation.Interfaces.Presenter;

public interface IPresenter<T>
{
    T GetById(Guid id);
    List<T> GetAll();
    void UpdateBlank(T entity);
    T GetBlank();
    bool DeleteById(Guid id, bool removeFromDb = false);
}