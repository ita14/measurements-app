/* tslint:disable */
/* eslint-disable */
/**
 * Measurements API
 * API to fetch ruuvi tag measurements data.
 *
 * The version of the OpenAPI document: 0.1.0
 * Contact: tbd@example.com
 *
 * NOTE: This class is auto generated by OpenAPI Generator (https://openapi-generator.tech).
 * https://openapi-generator.tech
 * Do not edit the class manually.
 */


import * as runtime from '../runtime';
import type {
  ProblemDetails,
  Sensor,
} from '../models';
import {
    ProblemDetailsFromJSON,
    ProblemDetailsToJSON,
    SensorFromJSON,
    SensorToJSON,
} from '../models';

export interface DeleteSensorRequest {
    id: string;
}

export interface GetSensorByIdRequest {
    id: string;
}

export interface PostSensorRequest {
    sensor: Sensor;
}

export interface PutSensorRequest {
    id: string;
    sensor: Sensor;
}

/**
 * 
 */
export class SensorsApi extends runtime.BaseAPI {

    /**
     * Delete sensor
     */
    async deleteSensorRaw(requestParameters: DeleteSensorRequest, initOverrides?: RequestInit | runtime.InitOverrideFunction): Promise<runtime.ApiResponse<void>> {
        if (requestParameters.id === null || requestParameters.id === undefined) {
            throw new runtime.RequiredError('id','Required parameter requestParameters.id was null or undefined when calling deleteSensor.');
        }

        const queryParameters: any = {};

        const headerParameters: runtime.HTTPHeaders = {};

        if (this.configuration && this.configuration.accessToken) {
            const token = this.configuration.accessToken;
            const tokenString = await token("bearerAuth", []);

            if (tokenString) {
                headerParameters["Authorization"] = `Bearer ${tokenString}`;
            }
        }
        const response = await this.request({
            path: `/sensors/{id}`.replace(`{${"id"}}`, encodeURIComponent(String(requestParameters.id))),
            method: 'DELETE',
            headers: headerParameters,
            query: queryParameters,
        }, initOverrides);

        return new runtime.VoidApiResponse(response);
    }

    /**
     * Delete sensor
     */
    async deleteSensor(requestParameters: DeleteSensorRequest, initOverrides?: RequestInit | runtime.InitOverrideFunction): Promise<void> {
        await this.deleteSensorRaw(requestParameters, initOverrides);
    }

    /**
     * Get sensor by id
     */
    async getSensorByIdRaw(requestParameters: GetSensorByIdRequest, initOverrides?: RequestInit | runtime.InitOverrideFunction): Promise<runtime.ApiResponse<Sensor>> {
        if (requestParameters.id === null || requestParameters.id === undefined) {
            throw new runtime.RequiredError('id','Required parameter requestParameters.id was null or undefined when calling getSensorById.');
        }

        const queryParameters: any = {};

        const headerParameters: runtime.HTTPHeaders = {};

        const response = await this.request({
            path: `/sensors/{id}`.replace(`{${"id"}}`, encodeURIComponent(String(requestParameters.id))),
            method: 'GET',
            headers: headerParameters,
            query: queryParameters,
        }, initOverrides);

        return new runtime.JSONApiResponse(response, (jsonValue) => SensorFromJSON(jsonValue));
    }

    /**
     * Get sensor by id
     */
    async getSensorById(requestParameters: GetSensorByIdRequest, initOverrides?: RequestInit | runtime.InitOverrideFunction): Promise<Sensor> {
        const response = await this.getSensorByIdRaw(requestParameters, initOverrides);
        return await response.value();
    }

    /**
     * Get sensors.
     */
    async getSensorsRaw(initOverrides?: RequestInit | runtime.InitOverrideFunction): Promise<runtime.ApiResponse<Array<Sensor>>> {
        const queryParameters: any = {};

        const headerParameters: runtime.HTTPHeaders = {};

        const response = await this.request({
            path: `/sensors`,
            method: 'GET',
            headers: headerParameters,
            query: queryParameters,
        }, initOverrides);

        return new runtime.JSONApiResponse(response, (jsonValue) => jsonValue.map(SensorFromJSON));
    }

    /**
     * Get sensors.
     */
    async getSensors(initOverrides?: RequestInit | runtime.InitOverrideFunction): Promise<Array<Sensor>> {
        const response = await this.getSensorsRaw(initOverrides);
        return await response.value();
    }

    /**
     * Create sensor.
     */
    async postSensorRaw(requestParameters: PostSensorRequest, initOverrides?: RequestInit | runtime.InitOverrideFunction): Promise<runtime.ApiResponse<Sensor>> {
        if (requestParameters.sensor === null || requestParameters.sensor === undefined) {
            throw new runtime.RequiredError('sensor','Required parameter requestParameters.sensor was null or undefined when calling postSensor.');
        }

        const queryParameters: any = {};

        const headerParameters: runtime.HTTPHeaders = {};

        headerParameters['Content-Type'] = 'application/json';

        if (this.configuration && this.configuration.accessToken) {
            const token = this.configuration.accessToken;
            const tokenString = await token("bearerAuth", []);

            if (tokenString) {
                headerParameters["Authorization"] = `Bearer ${tokenString}`;
            }
        }
        const response = await this.request({
            path: `/sensors`,
            method: 'POST',
            headers: headerParameters,
            query: queryParameters,
            body: SensorToJSON(requestParameters.sensor),
        }, initOverrides);

        return new runtime.JSONApiResponse(response, (jsonValue) => SensorFromJSON(jsonValue));
    }

    /**
     * Create sensor.
     */
    async postSensor(requestParameters: PostSensorRequest, initOverrides?: RequestInit | runtime.InitOverrideFunction): Promise<Sensor> {
        const response = await this.postSensorRaw(requestParameters, initOverrides);
        return await response.value();
    }

    /**
     * Update sensor
     */
    async putSensorRaw(requestParameters: PutSensorRequest, initOverrides?: RequestInit | runtime.InitOverrideFunction): Promise<runtime.ApiResponse<void>> {
        if (requestParameters.id === null || requestParameters.id === undefined) {
            throw new runtime.RequiredError('id','Required parameter requestParameters.id was null or undefined when calling putSensor.');
        }

        if (requestParameters.sensor === null || requestParameters.sensor === undefined) {
            throw new runtime.RequiredError('sensor','Required parameter requestParameters.sensor was null or undefined when calling putSensor.');
        }

        const queryParameters: any = {};

        const headerParameters: runtime.HTTPHeaders = {};

        headerParameters['Content-Type'] = 'application/json';

        if (this.configuration && this.configuration.accessToken) {
            const token = this.configuration.accessToken;
            const tokenString = await token("bearerAuth", []);

            if (tokenString) {
                headerParameters["Authorization"] = `Bearer ${tokenString}`;
            }
        }
        const response = await this.request({
            path: `/sensors/{id}`.replace(`{${"id"}}`, encodeURIComponent(String(requestParameters.id))),
            method: 'PUT',
            headers: headerParameters,
            query: queryParameters,
            body: SensorToJSON(requestParameters.sensor),
        }, initOverrides);

        return new runtime.VoidApiResponse(response);
    }

    /**
     * Update sensor
     */
    async putSensor(requestParameters: PutSensorRequest, initOverrides?: RequestInit | runtime.InitOverrideFunction): Promise<void> {
        await this.putSensorRaw(requestParameters, initOverrides);
    }

}
