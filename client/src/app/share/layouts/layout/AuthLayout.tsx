import { useEffect, useState } from 'react';
import { LayoutWithAuth } from '@ShareLayouts';
import {
  setIsAuth,
  UserStorage,
  setCurrentUserId,
} from '@ShareStores';
import { useDispatch } from 'react-redux';
import {
  Loading,
} from '@ShareUis'
import { useParams, useNavigate } from 'react-router-dom';
import { RouterAppEnum } from '@ShareEnums';

export function AuthLayout() {
  const [isLoading, setIsLoading] = useState(true);
  const { userId } = useParams();
  const navigate = useNavigate();
  const dispatch = useDispatch();

  async function doGetAuthStatus(id?: string) {
    if (id) {
      UserStorage.setCurrentUserById(id);
      dispatch(setCurrentUserId(id));
    }
    const rlt = UserStorage.getCurrentUserAuthStatus();
    dispatch(setIsAuth(rlt));
  }

  useEffect(() => {
    doGetAuthStatus(userId).finally(() => {
      if (userId) {
        navigate(RouterAppEnum.Base, { replace: true });
      }
      setIsLoading(false);
    })
  }, [userId])

  if (isLoading) {
    return (
      <Loading isShow={true} />
    )
  }

  return (
      <LayoutWithAuth />
  )
}

export default AuthLayout
