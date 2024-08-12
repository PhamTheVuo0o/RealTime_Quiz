export interface SidebarTitleProps {
    title: string;
}

export function SidebarTitle({ title }: SidebarTitleProps) {
    return (
        <p className="sidebar-menu-title text-color">{title}</p>
    );
}

export default SidebarTitle;