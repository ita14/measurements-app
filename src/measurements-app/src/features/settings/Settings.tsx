import React, { useEffect, useState } from 'react';
import { Spinner } from '../../components';
import { useForm } from 'react-hook-form';
import { Sensor } from '../../generated/measurements-api-client';
import {
  Button,
  Paper,
  TextField,
  Box,
  Container,
  IconButton
} from '@mui/material';
import DeleteIcon from '@mui/icons-material/Delete';
import { NewSensorDialog } from './NewSensorDialog';
import {
  useCreateSensor,
  useDeleteSensor,
  useGetSensors,
  useUpdateSensor
} from '../../api/sensors.api';

function Settings() {
  const [isOpen, setIsOpen] = useState(false);
  const methods = useForm({ mode: 'onChange' });
  const { handleSubmit, reset, formState, getValues, register } = methods;
  const { isDirty, isSubmitting, dirtyFields, errors } = formState;

  const { isLoading, isError, data, error } = useGetSensors();
  const updateSensor = useUpdateSensor();
  const createSensor = useCreateSensor();
  const deleteSensor = useDeleteSensor();

  // Set default values sensors change.
  useEffect(() => {
    const defaultValues = data?.reduce(
      (a, v) => ({
        ...a,
        [v.identifier ?? '']: v.description
      }),
      []
    );
    reset(defaultValues);
  }, [data]);

  const onSubmit = handleSubmit(async () => {
    const updated = Object.keys(dirtyFields).map<Sensor>((field: string) => ({
      id: data?.find((x) => x.identifier == field)?.id,
      identifier: field,
      description: getValues(field)
    }));

    await Promise.all(
      updated.map(async (sensor) => {
        await updateSensor.mutateAsync(sensor);
      })
    );

    // TODO: reset after submit
  });

  const canSubmit = () => isDirty && !isSubmitting;

  const handleAdd = async (sensor: Sensor) => {
    await createSensor.mutateAsync(sensor);
    setIsOpen(false);
  };

  const handleDelete = async (sensor: Sensor) => {
    if (sensor.id && window.confirm(`Delete ${sensor.description} ?`)) {
      await deleteSensor.mutateAsync(sensor.id);
    }
  };

  if (isLoading) {
    return <Spinner />;
  }

  if (isError) {
    // TODO: Error component
    return <div>{error?.detail}</div>;
  }

  return (
    <Container maxWidth="md">
      <NewSensorDialog
        isOpen={isOpen}
        onCancel={() => setIsOpen(false)}
        onCreate={handleAdd}
      />

      <Paper elevation={3} sx={{ padding: 2 }}>
        <h2>Sensors</h2>
        <Box component="form" onSubmit={onSubmit}>
          {data?.map((x) => (
            <Box
              key={x.identifier}
              sx={{ display: 'flex', paddingTop: 2, paddingBottom: 2 }}
            >
              <TextField
                {...register(x.identifier ?? '', { required: true })}
                id={x.identifier}
                name={x.identifier}
                label={x.identifier}
                variant="standard"
                margin="dense"
                error={!!errors[x.identifier ?? '']}
                helperText={
                  errors[x.identifier ?? '']
                    ? 'Description cannot be empty'
                    : ''
                }
                fullWidth
              />
              <IconButton onClick={() => handleDelete(x)}>
                <DeleteIcon />
              </IconButton>
            </Box>
          ))}
          <Box sx={{ display: 'flex', justifyContent: 'flex-end' }}>
            <Button
              variant="contained"
              disabled={!canSubmit()}
              onClick={() => reset()}
              sx={{ m: 1, p: 2 }}
            >
              Reset
            </Button>
            <Button
              variant="contained"
              color="primary"
              type="submit"
              disabled={!canSubmit()}
              sx={{ m: 1, p: 2 }}
            >
              Save
            </Button>
            <Button
              variant="contained"
              color="primary"
              onClick={() => setIsOpen(true)}
              sx={{ m: 1, p: 2 }}
            >
              Add
            </Button>
          </Box>
        </Box>
      </Paper>
    </Container>
  );
}

export default Settings;
