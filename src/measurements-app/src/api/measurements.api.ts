import { useQuery } from '@tanstack/react-query';
import {
  GetMeasurementsRequest,
  Measurement,
  MeasurementsApi,
  MeasurementsDataResponse,
  ProblemDetails
} from '../generated/measurements-api-client';

const api = new MeasurementsApi();

export function useGetMeasurements(
  source: string | undefined,
  startTime: Date,
  endTime: Date
) {
  return useQuery<Measurement[], ProblemDetails>({
    queryKey: ['measurements', source, startTime, endTime],
    queryFn: async () => {
      const request: GetMeasurementsRequest = {
        source,
        startTime,
        endTime,
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

function sleep(ms: number) {
  return new Promise((resolve) => setTimeout(resolve, ms));
}
