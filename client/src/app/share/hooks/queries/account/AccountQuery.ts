import { AccountService } from "@ShareServices";
import { UserStorage } from "@ShareStores";
import { useMutation } from '@tanstack/react-query';
import { AxiosError } from 'axios';
import { useDispatch, useSelector } from 'react-redux';
import {
    NavPIWithImageProps
}
    from '@ShareUis'
import {
    setIsAuth,
    setCurrentUserEmail,
    setCurrentUserName,
    setCurrentUserAvatar,
    setPermissions,
    RootState,
    setSignedInAccounts,
} from '@ShareStores';

export function useLogin() {
    const dispatch = useDispatch();
    const { mutate, isSuccess, isError, isPending, error } = useMutation({
        mutationFn: AccountService.Login,
        onSuccess: (data) => {
            if (data.isSuccess && data.data && data.data.isNotActive === false && data.data.token) {
                const tokenContent = UserStorage.getTokenContent(data.data.token).tokenContent;
                if (tokenContent) {
                    UserStorage.addUser(data.data.token, tokenContent.email);
                    dispatch(setIsAuth(true));
                    dispatch(setCurrentUserEmail(tokenContent.email));
                    dispatch(setCurrentUserName(`${tokenContent.firstName} ${tokenContent.lastName}`));
                    dispatch(setCurrentUserAvatar(tokenContent.avatar));
                }
            }
            return data;
        },
    });

    return {
        login: mutate,
        isSuccess,
        isError,
        isPending,
        isNotActive: (error as AxiosError<{ isNotActive: boolean }>)?.response
            ?.data?.isNotActive
    };
}

export function useGetCurrentUserProfile() {
    const dispatch = useDispatch();
    const currentUserEmail = useSelector((state: RootState) => state.GlobalVar.currentUserEmail);
    const signedInAccounts = useSelector((state: RootState) => state.GlobalVar.signedInAccounts);
    const { mutate, isSuccess, isError, isPending, error } = useMutation({
        mutationFn: AccountService.GetCurrentUserProfile,
        onSuccess: (data) => {
            if (data.isSuccess) {
                dispatch(setCurrentUserName(`${data.data.firstName} ${data.data.lastName}`));
                if (data.data.avatar) {
                    UserStorage.updateUser(
                        currentUserEmail,
                        undefined,
                        data.data.avatar
                    );
    
                    dispatch(setCurrentUserAvatar(data.data.avatar));
                    console.log("Get Avatar successfully")
    
                    const _signedInAccounts: NavPIWithImageProps[] = [];
                    signedInAccounts.forEach((item) => {
                        if (item.content.toLowerCase() === currentUserEmail.toLowerCase()) {
                            const _signedInAccount: NavPIWithImageProps = {
                                id: item.id,
                                image: data.data.avatar,
                                imageAlt: item.imageAlt,
                                title: item.title,
                                content: item.content,
                                link: item.link,
                                active: item.active,
                            };
                            _signedInAccounts.push(_signedInAccount);
                        }
                        else {
                            _signedInAccounts.push(item);
                        }
                    })
                    dispatch(setSignedInAccounts(_signedInAccounts));
                }
            }
            return data;
        },
    });
    return {
        GetCurrentUserProfile: mutate,
        isSuccess,
        isError,
        isPending,
        isUnAuth: (error as AxiosError<{ status: number }>)?.response?.data.status === 401,
    };
}
