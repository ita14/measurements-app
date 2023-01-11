import React from 'react';
import {
  Box,
  Button,
  Dialog,
  DialogActions,
  DialogContent,
  DialogContentText,
  DialogTitle,
  TextField
} from '@mui/material';
import { useForm } from 'react-hook-form';
import { Sensor } from '../../generated/measurements-api-client';

export interface NewSensorDialogProps {
  isOpen: boolean;
  onCreate: (sensor: Sensor) => void;
  onCancel: () => void;
}

export function NewSensorDialog({ isOpen, onCreate, onCancel }: NewSensorDialogProps) {
  const {
    handleSubmit,
    register,
    formState: { errors }
  } = useForm({ mode: 'onChange' });

  const onSubmit = handleSubmit((data) => {
    const newSensor: Sensor = {
      id: data.id,
      description: data.description
    };

    onCreate(newSensor);
  });

  const handleCancel = () => {
    onCancel();
  };

  return (
    <Dialog open={isOpen} onClose={handleCancel}>
      <DialogTitle>Add sensor</DialogTitle>
      <Box component="form" onSubmit={onSubmit}>
        <DialogContent>
          <DialogContentText>
            Give description and identifier for new sensor. Identifier must be unique.
          </DialogContentText>

          <TextField
            {...register('identifier', { required: true })}
            margin="dense"
            label="Identifier"
            variant="standard"
            error={!!errors.id}
            helperText={errors.id ? 'Field is mandatory' : ''}
            fullWidth
          />
          <TextField
            {...register('description', { required: true })}
            margin="dense"
            label="Description"
            variant="standard"
            error={!!errors.id}
            helperText={errors.id ? 'Field is mandatory' : ''}
            fullWidth
          />
        </DialogContent>
        <DialogActions>
          <Button onClick={handleCancel}>Cancel</Button>
          <Button type="submit">Ok</Button>
        </DialogActions>
      </Box>
    </Dialog>
  );
}
