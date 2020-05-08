using System;
using System.Collections.Generic;
using System.Text;

namespace BaltaStore.Shared.Commands
{
    public interface ICommandHandle<T> where T : ICommand
    {
        ICommandResult Handle(T command);
    }
}
