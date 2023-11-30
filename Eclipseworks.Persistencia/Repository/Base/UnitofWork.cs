﻿using Eclipseworks.Dominio.IRepository.Base;
using Eclipseworks.Dominio.Notifier.Interfaces;
using Eclipseworks.Persistencia.Contexto;
using Microsoft.EntityFrameworkCore;

namespace Eclipseworks.Persistencia.Repository.Base;

public class UnitofWork : IUnitofWork
{
    private readonly DbContext _context;
    private readonly INotifier _notifier;

    public UnitofWork(EclipseworksContext context, INotifier notifier)
    {
        _context = context;
        _notifier = notifier;
    }

    public DbContext Context => _context;

    public async Task Commit()
    {
        if (!_notifier.HasNotification)
        {
            await _context.SaveChangesAsync();
        }
    }


    public void Dispose()
    {
        _context.Dispose();
    }


}