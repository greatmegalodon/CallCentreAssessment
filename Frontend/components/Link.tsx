"use client";

import { PropsWithChildren } from "react";

import NextLink from "next/link";
import { LinkProps } from "next/link";

import Button from "./Button";

interface Props extends PropsWithChildren<LinkProps> {
  className?: string;
}

const Link = ({ children, href, className, ...props }: Props) => {
  return (
    <NextLink href={href} {...props}>
      <Button className={className}>{children}</Button>
    </NextLink>
  );
};

export default Link;
