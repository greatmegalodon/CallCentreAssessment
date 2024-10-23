"use client";

import classMerge from "@/util/classmerge";

interface Props extends React.HTMLAttributes<HTMLButtonElement> {
  className?: string;
  type?: "button" | "submit" | "reset";
}

const Button = ({ children, className, type, ...props }: Props) => {
  return (
    <button
      className={classMerge(
        "block rounded-md bg-black border border-gray-500 hover:border-gray-200 transition-all px-3 py-2 text-center text-sm font-semibold text-white focus-visible:outline focus-visible:outline-2 focus-visible:outline-offset-2 focus-visible:outline-white",
        className
      )}
      type={type}
      {...props}
    >
      {children}
    </button>
  );
};

export default Button;
