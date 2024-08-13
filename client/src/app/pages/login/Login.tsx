import { useState } from 'react';
import { Navigate } from "react-router-dom";
import { Label, CheckBox, TextInput, Image } from '@ShareUis';
import { useI18nContext } from '@ShareCores';
import { Trans } from 'react-i18next';
import { useLogin } from '@ShareHooks'
import './Login.scss';
import { useSelector } from 'react-redux';
import { RootState } from '@ShareStores';

export const Login = () => {
  const { t } = useI18nContext();
  const [email, setEmail] = useState('');
  const [isRememberMe, setIsRememberMe] = useState(false);
  const [password, setPassword] = useState('');
  const isAuth = useSelector((state: RootState) => state.GlobalVar.isAuth);

  const { login, isError, isNotActive } = useLogin();

  const handleLogin = () => {
    login({ email, password, isRememberMe });
  };

  if (isAuth) {
    return <Navigate to="/" />;
  }

  return (
    <div className='auth-form-light text-left py-5 px-4 px-sm-5 rounded-lg'>
        <div className="brand-logo">
          <img src="/icons/logo.svg" alt="logo" />
        </div>
        <h2>Sign in</h2>
        <form className="pt-3">
          {isError && (
            <Label className="text-primary mb-3">
              {isNotActive ? (
                t('inActiveAccountMessage')
              ) : (
                <Trans i18nKey="incorrectUsernameOrPassword">
                  <a className="text-link" href="/forgot-password"> </a>
                </Trans>
              )}
            </Label>
          )}
          <div className="form-group">
            <Label className='label-login' elementType='label' forInput='InputEmail'>{t('email')}</Label>
            <TextInput
              inputType="email"
              className="form-control form-control-lg rounded"
              id="InputEmail"
              placeholder="Username"
              value={email}
              onChange={(e) => setEmail(e.target.value)}
              rules={{
                required: t('pleaseEnterField', { fieldName: 'Email' }),
                pattern: {
                  value: /^[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,}$/i,
                  message: t('pleaseEnterValidField', { fieldName: 'Email' }),
                },
              }}
              bodyElement={document.body}
            />
          </div>
          <div className="form-group">
            <div className="my-2 d-flex justify-content-between align-items-center">
              <Label elementType='label' forInput='InputPassword' className="label-login form-check-input form-check ml-1">{t('password')}</Label>
              <a className="text-link" href="/forgot-password">Forgot password?</a>
            </div>
            <TextInput
              inputType="password"
              className="form-control form-control-lg rounded"
              id="InputPassword"
              placeholder="Password"
              value={password}
              onChange={(e) => setPassword(e.target.value)}
              rules={{
                required: t('pleaseEnterField', { fieldName: 'Password' }),
              }}
              bodyElement={document.body}
            />
          </div>
          <div className="my-2 d-flex justify-content-between align-items-center">
            <CheckBox className="form-check-input form-check ml-1"
              id="CheckBoxKMSI"
              checked={isRememberMe}
              label='Keep me signed in'
              onChange={() => setIsRememberMe(!isRememberMe)} />
          </div>
          <div className="mt-3">
            <button
              type="button"
              className="btn btn-block btn-primary btn-lg font-weight-medium auth-form-btn rounded"
              onClick={handleLogin}
            >
              SIGN IN
            </button>
          </div>
        </form>
      </div>
  );
};

export default Login;
