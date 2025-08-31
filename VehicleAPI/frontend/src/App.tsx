import { Link, Outlet, useLocation } from "react-router-dom";

export default function App() {
	const loc = useLocation();
    return (
        <div style={{ padding: 16}}>
			<h1>Vehicle App</h1>
			<nav style = {{ display: "flex", gap : 12, marginBottom: 16}}>
				<Link to="/makes" className={loc.pathname.startsWith("/makes")? "active" : "" }> Makes </Link>
				<Link to="/models" className={loc.pathname.startsWith("/models")? "active" : "" }> Models </Link>
				<Link to="/owners" className={loc.pathname.startsWith("/owners")? "active" : "" }> Owners </Link>
			</nav>
			<Outlet />
        </div>
    );
}