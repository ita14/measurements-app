import { useQuery } from '@tanstack/react-query';
import {
  GetMeasurementsRequest,
  Measurement,
  MeasurementsApi,
  MeasurementsDataResponse,
  ProblemDetails
} from '../generated/measurements-api-client';
import { apiConfig } from './auth';
import { isNumber } from 'lodash';

const api = new MeasurementsApi(apiConfig);

export function useGetMeasurements(
  source: string | undefined,
  startTime: Date | number,
  endTime: Date | number
) {
  return useQuery<Measurement[], ProblemDetails>({
    queryKey: ['measurements', source, startTime, endTime],
    queryFn: async () => {
      const request: GetMeasurementsRequest = {
        source,
        startTime: isNumber(startTime) ? new Date(startTime) : startTime,
        endTime: isNumber(endTime) ? new Date(endTime) : endTime,
        orderBy: 'time:asc'
      };

      request.offset = 0;
      request.limit = 1000;

      const measurements: Measurement[] = [];
      let response: MeasurementsDataResponse | null = null;

      do {
        response = await api.getMeasurements(request);

        if (response.items !== undefined) {
          measurements.push(...response.items);
        }

        request.offset += request.limit;
      } while (response.count == request.limit);
      return measurements;
    },
    onError: (e) => {
      console.error(e);
    },
    refetchOnWindowFocus: false
  });
}
