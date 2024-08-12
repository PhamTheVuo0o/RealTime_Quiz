export interface SidebarLegendItemProps {
    title: string;
    link: string;
    id: string;
}

export interface SidebarLegendProps {
    title: string;
    details: SidebarLegendItemProps[];
}

export function SidebarLegend({ title, details }: SidebarLegendProps) {
    if (details.length > 0) {
        return (
            <ul className="sidebar-legend">
                <li>
                    <p className="sidebar-menu-title text-color">{title}</p>
                </li>
                {details.map(item => (
                    <li className="nav-item"
                        key={item.id}
                    >
                        <a href={item.link} className=" text-color nav-link">
                            {item.title}
                        </a>
                    </li>
                ))}
            </ul>
        );
    }

}

export default SidebarLegend;