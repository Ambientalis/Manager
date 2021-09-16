using Quartz;
using Quartz.Impl;

/// <summary>
/// Descrição resumida de JobScheduler
/// </summary>
public class JobScheduler
{
    public static void Start()
    {
        IScheduler scheduler = StdSchedulerFactory.GetDefaultScheduler();
        scheduler.Start();

        IJobDetail jobOrcamento = JobBuilder.Create<ArquivamentoOrcamento>().Build();

        IJobDetail jobPedido = JobBuilder.Create<ArquivamentoPedido>().Build();


        ITrigger trigger = TriggerBuilder.Create()
              .WithIdentity("trigger1", "group1")
              .StartNow()
              .WithSimpleSchedule(x => x
              .WithIntervalInSeconds(120)
              .RepeatForever())
              .Build();


        scheduler.ScheduleJob(jobOrcamento, trigger);
        scheduler.ScheduleJob(jobPedido, trigger);

    }

}