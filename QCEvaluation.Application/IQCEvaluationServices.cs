using System;
using ApplicationServices;
using QCEvaluation.Application.DTOs;
using ICommand = ApplicationServices.ICommand;

namespace QCEvaluation.Application
{
    public interface IQCEvaluationServices:IHandler<ICommand,IResponse>, IHandler<IEvent>
    {
    }
}