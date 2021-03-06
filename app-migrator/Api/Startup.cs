using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GPConnectAdaptor;
using GPConnectAdaptor.AddAppointment;
using GPConnectAdaptor.Patient;
using GPConnectAdaptor.Practitioner;
using GPConnectAdaptor.ReadAppointments;
using GPConnectAdaptor.Slots;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // controllers
            services.AddControllers().AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );
            
            // serviceconfig
            services.AddScoped<IServiceConfig, ServiceConfig>();

            // common
            services.AddScoped<IOrchestrator, MigrationOrchestrator>();
            services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
            services.AddScoped<ITokenPayloadBuilder, TokenPayloadBuilder>();
            services.AddScoped<IEncoder, TokenEncoder>();
            services.AddScoped<IDateTimeGenerator, DateTimeGenerator>();
            
            // slots
            services.AddScoped<ISlotClient, SlotClient>();
            services.AddScoped<ISlotHttpClientWrapper, SlotHttpClientWrapper>();
            services.AddScoped<ISlotModelMapper, SlotModelMapper>();
            services.AddScoped<ISlotRetriever, SlotRetriever>();
            
            // add appointment
            services.AddScoped<IAddAppointmentClient, AddAppointmentClient>();
            services.AddScoped<IAddAppointmentHttpClientWrapper, AddAppointmentHttpClientWrapper>();
            services.AddScoped<IAddAppointmentRequestBuilder, AddAppointmentRequestBuilder>();
            services.AddScoped<IAddAppointmentRequestDeserializer, AddAppointmentRequestDeserializer>();
            services.AddScoped<IAppointmentBookedModelMapper, AppointmentBookedModelMapper>();
            
            // patient
            services.AddScoped<IPatientLookupHttpClientWrapper, PatientLookupHttpClientWrapper>();
            services.AddScoped<IPatientLookup, PatientLookup>();
            services.AddScoped<IPatientLookupClient, PatientLookupClient>();
            
            // practitioner
            services.AddScoped<IPractitionerLookupHttpClientWrapper, PractitionerLookupHttpClientWrapper>();
            services.AddScoped<IPractitionerLookup, PractitionerLookup>();
            services.AddScoped<IPractitionerLookupClient, PractitionerLookupClient>();
            
            // read appointment
            services.AddScoped<IReadAppointmentsHttpClientWrapper, ReadAppointmentsHttpClientWrapper>();
            services.AddScoped<IReadAppointmentsClient, ReadAppointmentsClient>();
            services.AddScoped<IAppointmentsResponseMapper, AppointmentsResponseMapper>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseHttpsRedirection();
            
            app.UseCors(
                options => 
                    options
                        .SetIsOriginAllowed(x => _ = true)
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials()
            );

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
         
        }
    }
}
