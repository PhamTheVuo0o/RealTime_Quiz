import './Loading.scss';
export interface LoadingProps {
    isShow:boolean;
    content?:string;
}
export function Loading({isShow, content}: LoadingProps) {
    if(isShow){
        if(content){
            return (
                <div className="LoaderBox">
                    <div className="Loader">
                        <div />
                        <div />
                        <div />
                        <div />
                    </div>
                    <p className="content-loading" >{content} ...</p>
                </div>
            );
        }
        else {
            return (
                <div className="LoaderBox">
                    <div className="Loader">
                        <div />
                        <div />
                        <div />
                        <div />
                    </div>
                </div>
            );
        }
    }
}

export default Loading;
