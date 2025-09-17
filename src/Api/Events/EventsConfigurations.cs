using JasperFx.Events.Projections;
using Marten;
using Marten.Events.Projections;
using Wolverine;

namespace Api.Events;

public class EventsMartenConfiguration : MartenModule
{
    public override void SetStoreOptions(StoreOptions options)
    {
        // Projections
        options.Projections.Snapshot<ProviderSchedule>(SnapshotLifecycle.Inline)
            .Identity(s => s.ProviderScheduleId);

        options.Projections.Add<AppointmentsTodayProjection>(ProjectionLifecycle.Inline);
        options.Projections.Add<OpenTasksProjection>(ProjectionLifecycle.Inline);
        options.Projections.Add<PracticeEventLogProjection>(ProjectionLifecycle.Inline);
        options.Projections.Add<AppointmentEventLogProjection>(ProjectionLifecycle.Inline);

        // Schemas
        options.Schema.For<ProviderSchedule>()
            .Identity(a => a.ProviderScheduleId)
            .MultiTenanted();

        options.Schema.For<ProviderAppointmentsTodayView>()
            .Identity(v => v.ProviderScheduleId)
            .MultiTenanted();

        options.Schema.For<OpenTasksView>()
            .Identity(v => v.AppointmentId)
            .MultiTenanted();

        options.Schema.For<OfficeRoomMapView>()
            .Identity(v => v.OfficeId)
            .MultiTenanted();

        // options.Schema.For<MapBase>()
        //     .Identity(v => v.OfficeId)
        //     .MultiTenanted();

        options.Schema.For<PracticeEventLog>()
            .Identity(v => v.Id)
            .MultiTenanted();

        options.Schema.For<AppointmentEventLog>()
            .Identity(ev => ev.Id)
            .MultiTenanted();
    }

}

public class EventsWolverineOptions : WolverineModule
{
    public override void ConfigureWolverineOptions(WolverineOptions options)
    {
        options.Policies.ForMessagesOfType<CheckWaitTimeHeartbeat>().AddMiddleware(typeof(PracticeLookupMiddleware));
        options.Discovery.IncludeType<UserLocationUpdated>();
        options.Discovery.IncludeType<ProviderScheduleStarted>();
    }
}