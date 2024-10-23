import { PropsWithChildren } from "react";
import { twMerge } from "tailwind-merge";

interface CardProps
  extends PropsWithChildren<React.HTMLAttributes<HTMLDivElement>> {
  className?: string;
}

const BaseCard = ({ className, children, ...props }: CardProps) => {
  return (
    <div
      className={twMerge(
        "flex flex-col space-y-0.5 py-7 px-12 border rounded-lg bg-gradient-to-br from-gray-950 to-black border-gray-800",
        className
      )}
      {...props}
    >
      {children}
    </div>
  );
};

interface CardTitleProps
  extends PropsWithChildren<React.HTMLAttributes<HTMLElement>> {
  className?: string;
}

const CardTitle = ({ className, children, ...props }: CardTitleProps) => {
  return (
    <h1 className={twMerge("text-4xl font-medium", className)} {...props}>
      {children}
    </h1>
  );
};

interface CardBodyProps
  extends PropsWithChildren<React.HTMLAttributes<HTMLParagraphElement>> {
  className?: string;
}

const CardBody = ({ className, children, ...props }: CardBodyProps) => {
  return (
    <p
      className={twMerge("text-xs text-gray-400 pl-0.5", className)}
      {...props}
    >
      {children}
    </p>
  );
};

export let Card = Object.assign(BaseCard, {
  Title: CardTitle,
  Body: CardBody,
});
