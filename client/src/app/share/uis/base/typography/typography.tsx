import clsx from 'clsx';
import React from 'react';

type DefaultVariantMappingType =
  | 'h-3xl'
  | 'h-2xl'
  | 'h1'
  | 'h2'
  | 'h3'
  | 'h4'
  | 'body-lg'
  | 'body-md'
  | 'body-sm'
  | 'body-xs';

interface DefaultVariantMappingProps {
  'h-3xl': string;
  'h-2xl': string;
  h1: string;
  h2: string;
  h3: string;
  h4: string;
  'body-lg': string;
  'body-md': string;
  'body-sm': string;
  'body-xs': string;
}

const defaultVariantMapping: DefaultVariantMappingProps = {
  'h-3xl': 'h-3xl',
  'h-2xl': 'h-3xl',
  h1: 'h4',
  h2: 'h2',
  h3: 'h3',
  h4: 'h4',
  'body-lg': 'div',
  'body-md': 'div',
  'body-sm': 'div',
  'body-xs': 'div',
};

export type ColorTransformationsType =
  | 'textPrimary'
  | 'textSecondary'
  | 'textTertiary'
  | 'primary'
  | 'critical';

interface ColorTransformationsProps {
  textPrimary: string;
  textSecondary: string;
  textTertiary: string;
  primary: string;
  critical: string;
}

const colorTransformations: ColorTransformationsProps = {
  textPrimary: 'text-neutral-foreground-primary',
  textSecondary: 'text-neutral-foreground-secondary',
  textTertiary: 'text-neutral-foreground-tertiary',
  primary: 'text-brand-500',
  critical: 'text-status-critical-foreground-rest',
};

const variantTransformations: DefaultVariantMappingProps = {
  'h-3xl': 'text-h-3xl',
  'h-2xl': 'text-h-2xl',
  h1: 'text-h1',
  h2: 'text-h2',
  h3: 'text-h3',
  h4: 'text-h4',
  'body-lg': 'text-body-lg',
  'body-md': 'text-body-md',
  'body-sm': 'text-body-sm',
  'body-xs': 'text-body-xs',
};

const transformDeprecatedVariant = (variant: DefaultVariantMappingType) => {
  return variantTransformations[variant] || variant;
};

const transformDeprecatedColors = (color: ColorTransformationsType) => {
  return colorTransformations[color] || color;
};

interface TypographyProps extends React.HTMLAttributes<HTMLElement> {
  /** Applies the theme typography styles. */
  variant: DefaultVariantMappingType;
  /** Set the color on the text. */
  color: ColorTransformationsType;
}

export function Typography({
  variant,
  color,
  children,
  className,
  ...rest
}: TypographyProps) {
  const element = defaultVariantMapping[variant];
  const classVariant = transformDeprecatedVariant(variant);
  const classText = transformDeprecatedColors(color);
  return React.createElement(
    element,
    {
      className: clsx(classVariant, classText, className),
      ...rest,
    },
    children
  );
}

Typography.defaultProps = {
  variant: 'body-md',
  color: 'textPrimary',
} as Required<TypographyProps>;

export default Typography;
