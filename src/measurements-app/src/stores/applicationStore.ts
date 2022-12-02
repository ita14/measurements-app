import create from 'zustand';
import { persist, devtools } from 'zustand/middleware';

export type Theme = 'dark' | 'light';

interface ApplicationState {
  theme: Theme;
  setTheme: (theme: Theme) => void;
}

export const useApplicationStore = create<ApplicationState>()(
  devtools(
    persist(
      (set) => ({
        theme: 'dark',
        setTheme: (theme) => set({ theme })
      }),
      {
        name: 'app-storage'
      }
    )
  )
);
