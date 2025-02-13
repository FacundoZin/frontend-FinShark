import { StrictMode } from 'react'
import { createRoot } from 'react-dom/client'
import './index.css'
import { searchCompanies } from './api.tsx'
import { Router, RouterProvider } from 'react-router'
import { rout } from './Routes/Routes.tsx'

console.log(searchCompanies("tsla"))
createRoot(document.getElementById('root')!).render(
  <StrictMode>
    <RouterProvider router={rout} />
  </StrictMode>
)
