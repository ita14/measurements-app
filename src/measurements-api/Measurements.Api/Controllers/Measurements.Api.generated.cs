//----------------------
// <auto-generated>
//     Generated using the NSwag toolchain v13.11.1.0 (NJsonSchema v10.4.3.0 (Newtonsoft.Json v12.0.0.0)) (http://NSwag.org)
// </auto-generated>
//----------------------

using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

#pragma warning disable 108 // Disable "CS0108 '{derivedDto}.ToJson()' hides inherited member '{dtoBase}.ToJson()'. Use the new keyword if hiding was intended."
#pragma warning disable 114 // Disable "CS0114 '{derivedDto}.RaisePropertyChanged(String)' hides inherited member 'dtoBase.RaisePropertyChanged(String)'. To make the current member override that implementation, add the override keyword. Otherwise add the new keyword."
#pragma warning disable 472 // Disable "CS0472 The result of the expression is always 'false' since a value of type 'Int32' is never equal to 'null' of type 'Int32?'
#pragma warning disable 1573 // Disable "CS1573 Parameter '...' has no matching param tag in the XML comment for ...
#pragma warning disable 1591 // Disable "CS1591 Missing XML comment for publicly visible type or member ..."
#pragma warning disable 8073 // Disable "CS8073 The result of the expression is always 'false' since a value of type 'T' is never equal to 'null' of type 'T?'"

namespace OpenApi.Measurements.Api
{
    using System = global::System;

    [System.CodeDom.Compiler.GeneratedCode("NSwag", "13.11.1.0 (NJsonSchema v10.4.3.0 (Newtonsoft.Json v12.0.0.0))")]
    public interface IMeasurementsController
    {
        /// <param name="startTime">Start time as defined by RFC 3339, section 5.6.</param>
        /// <param name="endTime">End time as defined by RFC 3339, section 5.6.</param>
        /// <param name="source">Measurement source identifier.</param>
        /// <param name="orderBy">Order results by column. Format is column_name:sort_direction.</param>
        /// <param name="limit">Maximum number of results to return</param>
        /// <param name="offset">Starting offset</param>
        /// <returns>Measurements response</returns>
        System.Threading.Tasks.Task<Microsoft.AspNetCore.Mvc.ActionResult<MeasurementsDataResponse>> MeasurementsAsync(System.DateTime? startTime, System.DateTime? endTime, string source, string orderBy, int limit, int offset, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
    
        /// <returns>OK</returns>
        System.Threading.Tasks.Task<Microsoft.AspNetCore.Mvc.IActionResult> BatchInsertAsync(System.Collections.Generic.IEnumerable<Measurement> body, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
    
    }
    
    [System.CodeDom.Compiler.GeneratedCode("NSwag", "13.11.1.0 (NJsonSchema v10.4.3.0 (Newtonsoft.Json v12.0.0.0))")]
    [ApiController]
    public partial class MeasurementsController : Microsoft.AspNetCore.Mvc.ControllerBase
    {
        private IMeasurementsController _implementation;
    
        public MeasurementsController(IMeasurementsController implementation)
        {
            _implementation = implementation;
        }
    
        /// <param name="startTime">Start time as defined by RFC 3339, section 5.6.</param>
        /// <param name="endTime">End time as defined by RFC 3339, section 5.6.</param>
        /// <param name="source">Measurement source identifier.</param>
        /// <param name="orderBy">Order results by column. Format is column_name:sort_direction.</param>
        /// <param name="limit">Maximum number of results to return</param>
        /// <param name="offset">Starting offset</param>
        /// <returns>Measurements response</returns>
        [Microsoft.AspNetCore.Mvc.HttpGet, Microsoft.AspNetCore.Mvc.Route("measurements", Name = "getMeasurements")]
        public System.Threading.Tasks.Task<Microsoft.AspNetCore.Mvc.ActionResult<MeasurementsDataResponse>> Measurements([Microsoft.AspNetCore.Mvc.FromQuery] System.DateTime? startTime, [Microsoft.AspNetCore.Mvc.FromQuery] System.DateTime? endTime, [Microsoft.AspNetCore.Mvc.FromQuery] string source, [Microsoft.AspNetCore.Mvc.FromQuery] string orderBy, [Microsoft.AspNetCore.Mvc.FromQuery] int? limit, [Microsoft.AspNetCore.Mvc.FromQuery] int? offset, System.Threading.CancellationToken cancellationToken)
        {
            return _implementation.MeasurementsAsync(startTime, endTime, source, orderBy ?? "time:asc", limit ?? 100, offset ?? 0, cancellationToken);
        }
    
        /// <returns>OK</returns>
        [Microsoft.AspNetCore.Mvc.HttpPost, Microsoft.AspNetCore.Mvc.Route("measurements/batch-insert", Name = "postMeasurements")]
        public System.Threading.Tasks.Task<Microsoft.AspNetCore.Mvc.IActionResult> BatchInsert([Microsoft.AspNetCore.Mvc.FromBody] [Microsoft.AspNetCore.Mvc.ModelBinding.BindRequired] System.Collections.Generic.IEnumerable<Measurement> body, System.Threading.CancellationToken cancellationToken)
        {
            return _implementation.BatchInsertAsync(body, cancellationToken);
        }
    
    }
    
    [System.CodeDom.Compiler.GeneratedCode("NSwag", "13.11.1.0 (NJsonSchema v10.4.3.0 (Newtonsoft.Json v12.0.0.0))")]
    public interface ISensorsController
    {
        /// <returns>Sensors response</returns>
        System.Threading.Tasks.Task<Microsoft.AspNetCore.Mvc.ActionResult<System.Collections.Generic.ICollection<Sensor>>> SensorsGetAsync(System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
    
        /// <returns>Sensor response</returns>
        System.Threading.Tasks.Task<Microsoft.AspNetCore.Mvc.ActionResult<Sensor>> SensorsPostAsync(Sensor body, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
    
        /// <param name="id">Id of sensor to fetch</param>
        /// <returns>Sensor response</returns>
        System.Threading.Tasks.Task<Microsoft.AspNetCore.Mvc.ActionResult<Sensor>> SensorsGetAsync(string id, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
    
        /// <param name="id">Id of sensor to update</param>
        /// <returns>Sensor updated</returns>
        System.Threading.Tasks.Task<Microsoft.AspNetCore.Mvc.IActionResult> SensorsPutAsync(Sensor body, string id, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
    
        /// <param name="id">Id of sensor to delete</param>
        /// <returns>Sensor deleted</returns>
        System.Threading.Tasks.Task<Microsoft.AspNetCore.Mvc.IActionResult> SensorsDeleteAsync(string id, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
    
    }
    
    [System.CodeDom.Compiler.GeneratedCode("NSwag", "13.11.1.0 (NJsonSchema v10.4.3.0 (Newtonsoft.Json v12.0.0.0))")]
    [ApiController]
    public partial class SensorsController : Microsoft.AspNetCore.Mvc.ControllerBase
    {
        private ISensorsController _implementation;
    
        public SensorsController(ISensorsController implementation)
        {
            _implementation = implementation;
        }
    
        /// <returns>Sensors response</returns>
        [Microsoft.AspNetCore.Mvc.HttpGet, Microsoft.AspNetCore.Mvc.Route("sensors", Name = "getSensors")]
        public System.Threading.Tasks.Task<Microsoft.AspNetCore.Mvc.ActionResult<System.Collections.Generic.ICollection<Sensor>>> SensorsGet(System.Threading.CancellationToken cancellationToken)
        {
            return _implementation.SensorsGetAsync(cancellationToken);
        }
    
        /// <returns>Sensor response</returns>
        [Microsoft.AspNetCore.Mvc.HttpPost, Microsoft.AspNetCore.Mvc.Route("sensors", Name = "postSensor")]
        public System.Threading.Tasks.Task<Microsoft.AspNetCore.Mvc.ActionResult<Sensor>> SensorsPost([Microsoft.AspNetCore.Mvc.FromBody] [Microsoft.AspNetCore.Mvc.ModelBinding.BindRequired] Sensor body, System.Threading.CancellationToken cancellationToken)
        {
            return _implementation.SensorsPostAsync(body, cancellationToken);
        }
    
        /// <param name="id">Id of sensor to fetch</param>
        /// <returns>Sensor response</returns>
        [Microsoft.AspNetCore.Mvc.HttpGet, Microsoft.AspNetCore.Mvc.Route("sensors/{id}", Name = "getSensorById")]
        public System.Threading.Tasks.Task<Microsoft.AspNetCore.Mvc.ActionResult<Sensor>> SensorsGet([Microsoft.AspNetCore.Mvc.ModelBinding.BindRequired] string id, System.Threading.CancellationToken cancellationToken)
        {
            return _implementation.SensorsGetAsync(id, cancellationToken);
        }
    
        /// <param name="id">Id of sensor to update</param>
        /// <returns>Sensor updated</returns>
        [Microsoft.AspNetCore.Mvc.HttpPut, Microsoft.AspNetCore.Mvc.Route("sensors/{id}", Name = "putSensor")]
        public System.Threading.Tasks.Task<Microsoft.AspNetCore.Mvc.IActionResult> SensorsPut([Microsoft.AspNetCore.Mvc.FromBody] [Microsoft.AspNetCore.Mvc.ModelBinding.BindRequired] Sensor body, [Microsoft.AspNetCore.Mvc.ModelBinding.BindRequired] string id, System.Threading.CancellationToken cancellationToken)
        {
            return _implementation.SensorsPutAsync(body, id, cancellationToken);
        }
    
        /// <param name="id">Id of sensor to delete</param>
        /// <returns>Sensor deleted</returns>
        [Microsoft.AspNetCore.Mvc.HttpDelete, Microsoft.AspNetCore.Mvc.Route("sensors/{id}", Name = "deleteSensor")]
        public System.Threading.Tasks.Task<Microsoft.AspNetCore.Mvc.IActionResult> SensorsDelete([Microsoft.AspNetCore.Mvc.ModelBinding.BindRequired] string id, System.Threading.CancellationToken cancellationToken)
        {
            return _implementation.SensorsDeleteAsync(id, cancellationToken);
        }
    
    }

    /// <summary>Measurement data from one source</summary>
    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "10.4.3.0 (Newtonsoft.Json v12.0.0.0)")]
    public partial class Measurement 
    {
        /// <summary>Measurement unique identifier. Generated on insert.</summary>
        [System.Text.Json.Serialization.JsonPropertyName("id")]
        public string Id { get; set; }
    
        /// <summary>Measurement time as defined by RFC 3339, section 5.6, for example, 2017-07-21T17:32:28Z</summary>
        [System.Text.Json.Serialization.JsonPropertyName("time")]
        public System.DateTime Time { get; set; }
    
        /// <summary>Source of measurement. With ruuvi tag this is MAC.</summary>
        [System.Text.Json.Serialization.JsonPropertyName("source")]
        public string Source { get; set; }
    
        /// <summary>Temperature in celsius.</summary>
        [System.Text.Json.Serialization.JsonPropertyName("temperature")]
        public double Temperature { get; set; }
    
        /// <summary>Pressure</summary>
        [System.Text.Json.Serialization.JsonPropertyName("pressure")]
        public double Pressure { get; set; }
    
        /// <summary>Humidity</summary>
        [System.Text.Json.Serialization.JsonPropertyName("humidity")]
        public double Humidity { get; set; }
    
        /// <summary>Battery level. TBD what is the format.</summary>
        [System.Text.Json.Serialization.JsonPropertyName("battery")]
        public double Battery { get; set; }
    
        [System.Text.Json.Serialization.JsonPropertyName("acceleration")]
        public Acceleration Acceleration { get; set; }
    
    
    }
    
    /// <summary>Acceleration</summary>
    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "10.4.3.0 (Newtonsoft.Json v12.0.0.0)")]
    public partial class Acceleration 
    {
        [System.Text.Json.Serialization.JsonPropertyName("acceleration")]
        public double Acceleration1 { get; set; }
    
        [System.Text.Json.Serialization.JsonPropertyName("accelerationX")]
        public double AccelerationX { get; set; }
    
        [System.Text.Json.Serialization.JsonPropertyName("accelerationY")]
        public double AccelerationY { get; set; }
    
        [System.Text.Json.Serialization.JsonPropertyName("accelerationZ")]
        public double AccelerationZ { get; set; }
    
    
    }
    
    /// <summary>Sensor producing measurements data</summary>
    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "10.4.3.0 (Newtonsoft.Json v12.0.0.0)")]
    public partial class Sensor 
    {
        /// <summary>Sensor unique identifier. Generated on insert.</summary>
        [System.Text.Json.Serialization.JsonPropertyName("id")]
        public string Id { get; set; }
    
        /// <summary>Sensor identifier. For ruuvi tags this is mac address. Must be unique.</summary>
        [System.Text.Json.Serialization.JsonPropertyName("identifier")]
        public string Identifier { get; set; }
    
        /// <summary>Sensor description.</summary>
        [System.Text.Json.Serialization.JsonPropertyName("description")]
        public string Description { get; set; }
    
    
    }
    
    /// <summary>Response object for measurements.</summary>
    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "10.4.3.0 (Newtonsoft.Json v12.0.0.0)")]
    public partial class MeasurementsDataResponse 
    {
        /// <summary>Count of items returned.</summary>
        [System.Text.Json.Serialization.JsonPropertyName("count")]
        public int Count { get; set; }
    
        /// <summary>Total number of items.</summary>
        [System.Text.Json.Serialization.JsonPropertyName("total")]
        public int Total { get; set; }
    
        [System.Text.Json.Serialization.JsonPropertyName("items")]
        public System.Collections.Generic.List<Measurement> Items { get; set; }
    
    
    }
    
    /// <summary>Problem details based on rfc7807.</summary>
    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "10.4.3.0 (Newtonsoft.Json v12.0.0.0)")]
    public partial class ProblemDetails 
    {
        [System.Text.Json.Serialization.JsonPropertyName("type")]
        public string Type { get; set; }
    
        [System.Text.Json.Serialization.JsonPropertyName("title")]
        public string Title { get; set; }
    
        [System.Text.Json.Serialization.JsonPropertyName("status")]
        public int? Status { get; set; }
    
        [System.Text.Json.Serialization.JsonPropertyName("detail")]
        public string Detail { get; set; }
    
        [System.Text.Json.Serialization.JsonPropertyName("instance")]
        public string Instance { get; set; }
    
        [System.Text.Json.Serialization.JsonPropertyName("extensions")]
        public System.Collections.Generic.IDictionary<string, object> Extensions { get; set; }
    
    
    }
    
    /// <summary>Problem details for validation errors.</summary>
    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "10.4.3.0 (Newtonsoft.Json v12.0.0.0)")]
    public partial class ValidationProblemDetails 
    {
        [System.Text.Json.Serialization.JsonPropertyName("errors")]
        public System.Collections.Generic.IDictionary<string, System.Collections.Generic.List<string>> Errors { get; set; }
    
        [System.Text.Json.Serialization.JsonPropertyName("type")]
        public string Type { get; set; }
    
        [System.Text.Json.Serialization.JsonPropertyName("title")]
        public string Title { get; set; }
    
        [System.Text.Json.Serialization.JsonPropertyName("status")]
        public int? Status { get; set; }
    
        [System.Text.Json.Serialization.JsonPropertyName("detail")]
        public string Detail { get; set; }
    
        [System.Text.Json.Serialization.JsonPropertyName("instance")]
        public string Instance { get; set; }
    
        [System.Text.Json.Serialization.JsonPropertyName("extensions")]
        public System.Collections.Generic.IDictionary<string, object> Extensions { get; set; }
    
    
    }

}

#pragma warning restore 1591
#pragma warning restore 1573
#pragma warning restore  472
#pragma warning restore  114
#pragma warning restore  108