﻿using System;

namespace DAL.Interfaces
{
    public interface IUnidadDeTrabajo : IDisposable
    {
        IClientDAL Clients { get; }
        IEmployeeDAL Employees { get; }
        IEventoDAL Eventos { get; }
        IEquipmentDAL Equipment { get; }
        IPaymentDAL Payments { get; }
        IRentalDAL Rentals { get; }
        IRentalDetailsDAL RentalDetails { get; }

        bool Complete();
    }
}
