import { useMutation, useQuery, useQueryClient } from '@tanstack/react-query';
import NotificationService from '../features/utils/notification-service';
import {
  ProblemDetails,
  Sensor,
  SensorsApi
} from '../generated/measurements-api-client';

const api = new SensorsApi();
const queryKey = ['sensors'];

export function useGetSensors() {
  return useQuery<Sensor[], ProblemDetails>({
    queryKey,
    queryFn: async () => {
      return await api.getSensors();
    },
    refetchOnWindowFocus: false
  });
}

export function useUpdateSensor() {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: (sensor: Sensor) => {
      return api.putSensor({ id: sensor.id, sensor });
    },
    onSuccess: () => queryClient.invalidateQueries({ queryKey }),
    onError: (e) => {
      NotificationService.error(`Failed to update sensor: ${e}`);
    }
  });
}

export function useDeleteSensor() {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: (id: string) => api.deleteSensor({ id }),
    onSettled: () => queryClient.invalidateQueries({ queryKey }),
    onError: (e) => {
      NotificationService.error(`Failed to delete sensor: ${e}`);
    }
  });
}

export function useCreateSensor() {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: (sensor: Sensor) => api.postSensor({ sensor }),
    onSuccess: () => queryClient.invalidateQueries({ queryKey }),
    onError: (e) => {
      NotificationService.error(`Failed to create sensor: ${e}`);
    }
  });
}
