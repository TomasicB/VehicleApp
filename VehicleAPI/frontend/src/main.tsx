import React from "react";
import ReactDOM from "react-dom/client";
import { Provider } from 'react-redux';
import { store } from './store';
import { BrowserRouter, Routes, Route } from "react-router-dom";

import App from "./App";
import MakeList from "./pages/makes/vehicleMakeList";
import MakeEdit from "./pages/makes/vehicleMakeEdit";
import ModelList from "./pages/models/vehicleModelList";
import ModelEdit from "./pages/models/vehicleModelEdit";
import OwnerList from "./pages/owners/vehicleOwnerList";
import OwnerEdit from "./pages/owners/vehicleOwnerEdit";
import "./index.css";


ReactDOM.createRoot(document.getElementById('root')!).render(
  <React.StrictMode>
    <Provider store={store}>
      <BrowserRouter>
        <Routes>
          <Route path="/" element={<App />}>
            <Route index element={<MakeList />} />
            <Route path="makes" element={<MakeList />} />
            <Route path="makes/:id" element={<MakeEdit />} />
            
            <Route path="models" element={<ModelList />} />
            <Route path="models/:id" element={<ModelEdit />} />
            
            <Route path="owners" element={<OwnerList />} />
            <Route path="owners/:id" element={<OwnerEdit />} />
          </Route>
        </Routes>
      </BrowserRouter>
    </Provider>
  </React.StrictMode>
);