import { useEffect, useState } from 'react';
import { Navigate, Outlet } from "react-router-dom";
import { NavMenu } from '@ShareLayouts';
import { Loading } from '@ShareUis'
import { useSelector } from 'react-redux';
import { RootState } from '@ShareStores';
import { useGetCurrentUserProfile } from '@ShareHooks'

export function LayoutWithAuth() {
  const isLoading = useSelector((state: RootState) => state.GlobalVar.isLoading);
  const isAuth = useSelector((state: RootState) => state.GlobalVar.isAuth);
  const { GetCurrentUserProfile } = useGetCurrentUserProfile();
  const [isRun, setIsRun] = useState(false);
  const currentUserId = useSelector((state: RootState) => state.GlobalVar.currentUserId);
  const [oldUserId, setOldUserId] = useState(currentUserId);

  function UserProfileLoading() {
    if ((!isRun)||(oldUserId != currentUserId)) {
      setIsRun(true);
      setOldUserId(currentUserId);
      GetCurrentUserProfile();
    }
  }

  useEffect(() => {
    UserProfileLoading();
  }, [currentUserId])

  if (!isAuth) {
    return <Navigate to="/login" />;
  }

  return (
    <>
      <NavMenu >
        <Outlet />
      </NavMenu>
      <Loading isShow={isLoading} content="Loading" />
    </>

  )
}

export default LayoutWithAuth
