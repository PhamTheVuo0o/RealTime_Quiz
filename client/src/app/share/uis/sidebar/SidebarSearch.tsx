import { useState, useEffect, useRef, KeyboardEvent } from "react";
import { useI18nContext } from '@ShareCores';
export interface SidebarSearchProps {
    onChangeSearchText: React.Dispatch<React.SetStateAction<string>>;
    doSearch: () => void;
}

export function SidebarSearch({ onChangeSearchText, doSearch }: SidebarSearchProps) {
    const { t } = useI18nContext();
    const [searchValue, setSearchValue] = useState("");
    const previousSearchValue = useRef("");

    useEffect(() => {
        if(previousSearchValue.current != searchValue){
            doSearch();
        }
        previousSearchValue.current = searchValue;
    }, [searchValue]);

    function UpdateValue(value: string) {
        onChangeSearchText(value);
        setSearchValue(value);
    }

    const handleKeyUp = (event: KeyboardEvent<HTMLInputElement>) => {
        if (event.key === 'Enter') {
            doSearch();
        }
    };

    return (
        <div className="nav-search">
            <div className="input-group">
                <input
                    type="text"
                    className="form-control text-color"
                    placeholder={t('searchPlaceholder')}
                    aria-label="search"
                    aria-describedby="search"
                    onKeyUp={handleKeyUp}
                    onChange={(e) => UpdateValue(e.target.value)}
                />
                <div className="input-group-append" onClick={doSearch}>
                    <span className="input-group-text" id="search">
                        <i className="typcn typcn-zoom text-color" />
                    </span>
                </div>
            </div>
        </div>
    );
}

export default SidebarSearch;